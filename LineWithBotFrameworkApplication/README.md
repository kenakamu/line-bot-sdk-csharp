# LineWithBotFrameworkApplition
Visual Studio 2015 project template which connects Line Messaging Web API to Microsoft Bot Framework via Directline.
If you already have Bot Application up and running at Microsoft Bor Framework, then use this tempalte.

About the LINE Messaging API
------------------------

See the official API documentation for more information.

English: https://devdocs.line.me/en/ <br/>
Japanese: https://devdocs.line.me/ja/

## Install
You can download from https://github.com/kenakamu/line-bot-sdk-csharp/releases

1. Download Line Bot Application.zip from [here](https://github.com/kenakamu/line-bot-sdk-csharp/releases) and 
save the zip file to your Visual Studio 2015 templates directory which is traditionally in
"%USERPROFILE%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\"
2. Open Visual Studio 2015
3. Create a new C# project using the new Line Bot Application template. 
4. Update ChannelSecret, ChannelToken and DirectLine Secrete in Web.Config file. <br/>
You can get there values from https://business.line.me and https://dev.botframework.com

## Update Bot Framework Code
As directline doesn't support several features, you need to move Attachments or Entity to ChannelData.

#### Carousel template
Simply move Attachements to ChannelData by using following code.
```csharp
// Assume you already create reply which contains carousel as Attachments 
if (channelId.Contains("direct")) 
{
  // Create new reply message 
  var replyForLine = context.MakeMessage();
  // Copy Attachment from original message to ChannelData
  // Mark the type as "Rich"
  Entity heroCard = new Entity() { 
    Type = "Rich", 
    Properties = new JObject(new JProperty("Attachments", JArray.FromObject(reply.Attachments))) };                
  replyForLine.ChannelData = JsonConvert.SerializeObject(heroCard);
}
```

#### Confirm Template
Use the same sample above but specify "Confirm" as Type.

#### Location and GeoCoordinates
In BotFramework, you set Location and GeoCoordinates as Entity. Place them into ChannelData and mark Type as "Location" or "GeoCoordinates" depending on type.
```csharp
// Assume you already create reply which contains place or geocoordinates as Entity
if (channelId.Contains("direct")) 
{
  // Copy Entity from original message to ChannelData   
  reply.ChannelData = JsonConvert.SerializeObject(entity);
}
```

# Template features
Basically, you dont neet to do anything when using this templates as it simply redirect request frm Line Platform to Microsoft Bot Framework.

### Varidate Signature
When receiving Post message from Line Platform, it verifies the request by following the documentation at https://devdocs.line.me/en/#signature-validation

### Parse and handle Incoming message
Parse the incoming Post body and handle event. LineMessageHandler class contains methods to handle each type.

### Redirect to DirectLine
Create appropriate message and send it to DirectLine.

### Get reply from DirectLine
Once message redirected, then receiving message.

### Reply/Push message
Finally, it replies to Line Platform by using either reply or push. Find detail information here.

Reply Message: https://devdocs.line.me/en/#reply-message <br/>
Push Message: https://devdocs.line.me/en/#push-message
