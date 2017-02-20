from Authorization import RSAv3
import os
import logging
import settings

settings.init()

class Decoder:

    workfolder = settings.workfolder
    path_encrypted_folder = workfolder + '/msg'
    path_decoded_folder = workfolder + '/decoded'
    files = []
    sucess = []

    def decode(self):
        self.files = os.listdir(self.path_encrypted_folder)

        try:
            for file in self.files:
                logging.info('Reading file: {}'.format(file))
                f = open(self.path_encrypted_folder + '/' + file, 'rb')
                data = f.read()
                f.close()

                logging.info('Decrypting')
                decrypted = RSAv3.decrypt(data)
                self.sucess.append(file)

                logging.info('Saving decrypted: {}'.format(decrypted.__len__()))
                f = open(self.path_decoded_folder + '/' + file, 'wb')
                f.write(decrypted)
                f.close()
        except TypeError as e:
            logging.info('I can\'t Write this Type to file. There was an error in decrypting propably')
        except Exception as e:
            logging.error('An error in decoding file.')


    def delete_decoded_files(self):
        deleted = []
        try:
            for f in self.sucess:
                os.remove(self.path_encrypted_folder + '/' + f)
                deleted.append(f)
        except Exception as ex:
            logging.error('Błąd przy usuwaniu plików po rozszyfrowywaniu. {}'.format(ex))
        for f in deleted:
            self.sucess.remove(f)
