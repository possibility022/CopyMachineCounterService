using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MetCounterService.Security
{
    class RSAio
    {
        byte[] rsaKeyPair = Convert.FromBase64String("30819D300D06092A864886F70D010101050003818B0030818702818100C6F80A0CA603136DA265397481E6B00A0BFB0A2D928A139D0E179960E382B7DB1F9A39DB7CA65E113C7BE37DCCD7EA731B4C1722A8C0BB3E38675E15CD4B1344732C09770BDFE2C8028B2BE017168FB5E2AF4F67209A8F663EC4D2F59C70F60F75B683063F55C9499ABABAE5A56902B74CEAFD64BB0D416E5B00A448D2DBF8FB020111");
        public RSAio()
        {
            var csp = new RSACryptoServiceProvider(1024);

            //how to get the private key
            var privKey = csp.ExportParameters(true);

            //and the public key ...
            var pubKey = csp.ExportParameters(false);

            //we have a public key ... let's get a new csp and load that key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey);

            var plainTextData = "foobar";
            //for encryption, always handle bytes...
            var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

            //we might want a string representation of our cypher text... base64 will do
            var cypherText = Convert.ToBase64String(bytesCypherText);

            //first, get our bytes back from the base64 string ...
            bytesCypherText = Convert.FromBase64String(cypherText);

            //we want to decrypt, therefore we need a csp and load our private key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privKey);

            //decrypt and strip pkcs#1.5 padding
            bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

            //get our original plainText back...
            plainTextData = System.Text.Encoding.Unicode.GetString(bytesPlainTextData);
        }

    }
}
