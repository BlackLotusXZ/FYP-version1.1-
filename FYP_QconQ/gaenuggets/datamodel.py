from google.appengine.ext import db



class StoryModeStats(db.Model):
	"""Models a story mode stats entity with a device ID, nickname, score and a timestamp"""
	deviceID = db.StringProperty(required=True)
	nickname = db.StringProperty(required=True)
	score = db.IntegerProperty(required = True)
	timestamp = db.StringProperty(required=True)
	
	
class ArcadeModeStats(db.Model):
	"""Models a arcade mode stats entity with a device ID, nickname, attempts and a timestamp"""
	deviceID = db.StringProperty(required=True)
	nickname = db.StringProperty(required=True)
	attempts = db.IntegerProperty(required=True)
	timestamp = db.StringProperty(required=True)