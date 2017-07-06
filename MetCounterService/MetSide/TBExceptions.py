import datetime
import settings
import os

class ServerException(Exception):
    
	def __init__(self, *args):
		self.loggedsteps = []
		settings.init()

	def add_step(self, value):
		self.loggedsteps.append(value)

	def save_log_to_file(self):
		filename = str(datetime.date.day) + str(datetime.date.month) + str(datetime.date.year) + str(datetime.datetime.hour) + str(datetime.datetime.minute)
		filename = filename + '.log'

		f = open(settings.workfolder + '/' + filename, 'rw')
		for el in self.loggedsteps:
			f.write(el + os.linesep)
		f.close

		return filename