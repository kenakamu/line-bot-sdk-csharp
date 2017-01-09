# line-bot-sdk-csharp
SDK of the LINE Messaging API for C#

About the LINE Messaging API
------------------------

See the official API documentation for more information.

English: https://devdocs.line.me/en/ <br/>
Japanese: https://devdocs.line.me/ja/

##Install
You can install package from Nuget.
> Install-Package LineMessagingAPI.CSharp

## Visual Studio Template
This repository contains Visual Studio template which includes the library.<br/>
You can download from https://github.com/kenakamu/line-bot-sdk-csharp/releases

1. Download Line Bot Application.zip save the zip file to your Visual Studio 2015 templates directory which is traditionally in "%USERPROFILE%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\"
2. Open Visual Studio 2015Create a new C# project using the new Line Bot Application template. 
3. Update ChannelSecret and ChannelToken in Web.Config file. You can get there values from https://business.line.me

# Template features
This templates does followings.
### Varidate Signature
When receiving Post message from Line Platform, it verifies the request by following the documentation at <br/> 
https://devdocs.line.me/en/#signature-validation

### Parse and handle Incoming message
Parse the incoming Post body and handle each type.

### Create reply message sample
Several types such as Text, Location or Media type of message already has sample reply implemented.

### Reply/Push message
Finally, it replies to Line Platform by using either reply or push. Find detail information here.

Reply Message: https://devdocs.line.me/en/#reply-message <br/>
Push Message: https://devdocs.line.me/en/#push-message

# Create Replymessage
Following examples demonstrate how to construct reply message.

#### Text Reply
```csharp 
var replyMessage = new TextMessage(textMessage.Text);
```

#### Image Reply
```csharp       
var replyMessage = new ImageMessage("https://github.com/apple-touch-icon.png", "https://github.com/apple-touch-icon.png");
```

#### Audio Reply
```csharp
var replyMessage = new AudioMessage("pass to mp4 file", 3600);
```

#### Video Reply
```csharp
var replyMessage = new VideoMessage("pass to video", "pass to video thumbnail");
```

#### Sticker Reply
```csharp
var replyMessage = new StickerMessage("1","1");
```

#### Location Reply
```csharp
var replyMessage = new LocationMessage("Title","Address","<latitude>","<longitude>");
```

#### Button Template Reply
```csharp
List<TemplateAction> actions = new List<TemplateAction>();
actions.Add(new MessageTemplateAction("Message Label", "sample data")); 
actions.Add(new PostbackTemplateAction("Postback Label", "sample data"));
actions.Add(new UriTemplateAction("Uri Label", "https://github.com/kenakamu"));

ButtonsTemplate buttonsTemplate = new ButtonsTemplate("https://github.com/apple-touch-icon.png", "Sample Title", "Sample Text", actions);                
var replyMessage = new TemplateMessage("Buttons", buttonsTemplate);
```

#### Confirm Template Reply
```csharp
List<TemplateAction> actions = new List<TemplateAction>();
actions.Add(new MessageTemplateAction("Yes", "yes"));
actions.Add(new MessageTemplateAction("No", "no"));

ConfirmTemplate confirmTemplate = new ConfirmTemplate("Confirm Test", actions);
replyMessage = new TemplateMessage("Confirm", confirmTemplate);
```

#### Carousel Template Reply
```csharp
List<TemplateColumn> columns = new List<TemplateColumn>();
List<TemplateAction> actions = new List<TemplateAction>(); 
actions.Add(new MessageTemplateAction("Message Label", "sample data"));
actions.Add(new PostbackTemplateAction("Postback Label", "sample data"));
actions.Add(new UriTemplateAction("Uri Label", "https://github.com/kenakamu"));               

columns.Add(new TemplateColumn() { Title = "Casousel 1 Title", Text = "Casousel 1 Text", ThumbnailImageUrl = "https://github.com/apple-touch-icon.png", Actions = actions });   
columns.Add(new TemplateColumn() { Title = "Casousel 2 Title", Text = "Casousel 2 Text", ThumbnailImageUrl = "https://github.com/apple-touch-icon.png", Actions = actions }); 

CarouselTemplate carouselTemplate = new CarouselTemplate(columns);                
replyMessage = new TemplateMessage("Carousel", carouselTemplate);
```

