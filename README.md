# HelloWorldRansom

To rebuild the appsettings.json file it should look like this:

{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "Config": {
    "FlickrApiKey": "XXXXXXXXXXXXXXXXXXXXX",
    "MicrosoftTranslateApiKey": "XXXXXXXXXXXXXXXXXXXXXXX"
  }
}


Where the XXXXX's are your api keys.

The flickr api key can be requested here:
https://www.flickr.com/services/apps/create/apply

To get the Microsoft Translator Key you will need an Azure account and to create a new Translator Text API under the Congnitive Services section.
Then you can get a key from the Keys page under Resource Management.
