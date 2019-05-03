using System;
using CopyinfoWPF.Common;
using CopyinfoWPF.Security.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CopyinfoWPF.Security.UnitTests
{
    [TestClass]
    public class ObjectEncryptorTests
    {

        ObjectEncryptor objectEncryptor;

        Mock<IEncrypting> Encrypting;
        Mock<IByteSerializer> ByteSerializer;

        [TestInitialize]
        public void TestInit()
        {
            Encrypting = new Mock<IEncrypting>();
            ByteSerializer = new Mock<IByteSerializer>();

            objectEncryptor = new ObjectEncryptor(Encrypting.Object, ByteSerializer.Object);
        }

        [TestMethod]
        public void EncryptIsCallingExactlyThreeTimes()
        {
            var test = new TestingClass
            {
                PropA = 123,
                PropB = "str",
                PropC = new byte[] { 1, 5, 1, 2 },
                PropD = "str2"
            };

            // ACT
            var json = objectEncryptor.Encrypt(test);

            Encrypting.Verify(r => r.Protect(It.IsAny<byte[]>()), Times.Exactly(3));
            ByteSerializer.Verify(r => r.Serialize(It.IsAny<object>()), Times.Exactly(3));
        }

        [TestMethod]
        public void DecryptingIsCallingExactlyThreeTimes()
        {
            var json = "{  \"PropA\": \"AQUBAg==\",  \"PropB\": \"AQUBAg==\",  \"PropC\": \"AQUBAg==\",  \"PropD\": \"str2\"}";

            ByteSerializer.Setup(r => r.Deserialize(It.IsAny<byte[]>(), It.Is<Type>(t => t == typeof(string))))
                .Returns(string.Empty);

            ByteSerializer.Setup(r => r.Deserialize(It.IsAny<byte[]>(), It.Is<Type>(t => t == typeof(int))))
                .Returns(0);

            ByteSerializer.Setup(r => r.Deserialize(It.IsAny<byte[]>(), It.Is<Type>(t => t == typeof(byte[]))))
                .Returns(new byte[] { });


            var obj = objectEncryptor.Decrypt<TestingClass>(json);

            ByteSerializer.Verify(s => s.Deserialize(It.IsAny<byte[]>(), It.IsAny<Type>()), Times.Exactly(3));
            Encrypting.Verify(s => s.Unprotect(It.IsAny<byte[]>()), Times.Exactly(3));

        }
    }

    class TestingClass
    {
        [Encrypt]
        public int PropA { get; set; }
        [Encrypt]
        public string PropB { get; set; }
        [Encrypt]
        public byte[] PropC { get; set; }
        public string PropD { get; set; }
    }

}
