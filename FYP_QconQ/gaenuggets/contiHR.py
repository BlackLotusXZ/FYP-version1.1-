import cgi
import datetime
import urllib
import wsgiref.handlers
import os
import logging

from google.appengine.ext.webapp import template
from google.appengine.ext import db
from google.appengine.api import users
import webapp2
import datamodel
import config

DMessageList = [
		'[Welcome to Q.Nect!]',
		'[These are messages from the server!]',
		'[Earn gold to purchase items in shop]',
		]
		


class MainPage(webapp2.RequestHandler):
    def get(self):	
		#values is a dictionary matched with the path(below) which helps with the html/css, uses Django templating syntax
		template_values = {
			'DMessageList' : DMessageList,
		}

		#path is a path to the html file that handles the rendering for this section
		path = os.path.join(os.path.dirname(__file__), 'html/index.html')
		#this line links the path with the values and renders them according to Django templating syntax
		self.response.out.write(template.render(path, template_values))
			
class StoryModeStatsPost(webapp2.RequestHandler):
	def post(self):
		"""Grab the info from the POST action from Unity"""
		deviceID1 = self.request.get('deviceID')
		nickname1 = self.request.get('nickname')
		score1 = int(self.request.get('score'))
		timestamp1 = datetime.datetime.now().strftime("%A, %d %B %Y %I:%M%p")
		
		"""Datastore processing part"""
		PlayerstatsEntry = datamodel.StoryModeStats(key_name = self.request.get('deviceID'), deviceID = deviceID1, nickname = nickname1, score = score1, timestamp = timestamp1)
		#print in python is logging.info
		k = PlayerstatsEntry.put()
		p2 = datamodel.StoryModeStats.get(k)
		if config.isReset:
			config.isReset = False

		
class StoryModeStatsGet(webapp2.RequestHandler):
	def get(self):		
		stats_query = datamodel.StoryModeStats.all()
		stats_query.order('-score')
		stats = stats_query.fetch(5)
		
		template_values = {
			'isReset' : config.isReset,
			'stats' : stats,
		}
		
		path = os.path.join(os.path.dirname(__file__), 'html/storystats.html')
		self.response.out.write(template.render(path,template_values))
		
class ArcadeModeStatsPost(webapp2.RequestHandler):
	def post(self):
		deviceID1 = self.request.get('deviceID')
		nickname1 = self.request.get('nickname')
		attempts1 = int(self.request.get('attempts'))
		timestamp1 = datetime.datetime.now().strftime("%A, %d %B %Y %I:%M%p")
		PlayerstatsEntry = datamodel.ArcadeModeStats(key_name = self.request.get('deviceID'), deviceID = deviceID1, nickname = nickname1, attempts = attempts1, timestamp = timestamp1)
		PlayerstatsEntry.put()
		if config.isReset:
			config.isReset = False
		
class ArcadeModeStatsGet(webapp2.RequestHandler):
	def get(self):
		stats_query = datamodel.ArcadeModeStats.all()
		stats_query.order('-attempts')
		stats = stats_query.fetch(5)
		
		template_values = {
			'isReset' : config.isReset,
			'stats' : stats,
		}
		
		path = os.path.join(os.path.dirname(__file__), 'html/arcadestats.html')
		self.response.out.write(template.render(path,template_values))
		
class resetserver(webapp2.RequestHandler):
	def get(self):
		db.delete(datamodel.StoryModeStats.all())
		db.delete(datamodel.ArcadeModeStats.all())
		
		config.isReset = True
		
		timestamp1 = datetime.datetime.now().strftime("%A, %d %B %Y %I:%M%p")
		#StoryModeStats
		PlayerstatsEntry = datamodel.StoryModeStats(key_name = "1234567890", deviceID = "1234567890", nickname = "Tom", score = 100, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.StoryModeStats(key_name = "1234567891", deviceID = "1234567891", nickname = "Ben", score = 150, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.StoryModeStats(key_name = "1234567892", deviceID = "1234567892", nickname = "Jane", score = 200, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.StoryModeStats(key_name = "1234567893", deviceID = "1234567893", nickname = "Mary", score = 250, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.StoryModeStats(key_name = "1234567894", deviceID = "1234567894", nickname = "John", score = 300, timestamp = timestamp1)
		PlayerstatsEntry.put()
		
		#ArcadeModeStats
		PlayerstatsEntry = datamodel.ArcadeModeStats(key_name = "1234567890", deviceID = "1234567890", nickname = "Tom", attempts = 5, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.ArcadeModeStats(key_name = "1234567891", deviceID = "1234567891", nickname = "Ben", attempts = 10, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.ArcadeModeStats(key_name = "1234567892", deviceID = "1234567892", nickname = "Jane", attempts = 15, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.ArcadeModeStats(key_name = "1234567893", deviceID = "1234567893", nickname = "Mary", attempts = 20, timestamp = timestamp1)
		PlayerstatsEntry.put()
		PlayerstatsEntry = datamodel.ArcadeModeStats(key_name = "1234567894", deviceID = "1234567894", nickname = "John", attempts = 25, timestamp = timestamp1)
		PlayerstatsEntry.put()
	
app = webapp2.WSGIApplication([
	('/', MainPage),
	('/storystatspost', StoryModeStatsPost),
	('/arcadestatspost', ArcadeModeStatsPost),
	('/storystatsget', StoryModeStatsGet),
	('/arcadestatsget', ArcadeModeStatsGet),
	('/resetserver', resetserver),
], debug=True)

def main():
	app.run()

if __name__ == "__main__":
	main()