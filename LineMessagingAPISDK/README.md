# LineMessagingAPISDK 
SDK of the LINE Messaging API for C#.

About the LINE Messaging API
------------------------

See the official API documentation for more information.

English: https://devdocs.line.me/en/ <br/>
Japanese: https://devdocs.line.me/ja/

##Install
You can install package from Nuget.
> Install-Package LineMessagingAPI.CSharp

You can also use Visual Studio 2015 Template for jump start. <br/>
See [here](https://github.com/kenakamu/line-bot-sdk-csharp/tree/master/LineBotApplication) for more detail.

# LineClient
LineClient class is the main proxy class.

#### Instantiate
```csharp
LineClient lineClient = new LineClient("<Channel Access Token>");
```
#### Get Meida Content
To download image, video or audio, use GetContent method. See https://devdocs.line.me/en/#get-content for more detail.
```csharp
Media media = await lineClient.GetContent("<content id>");
```

#### Get Line User Profile
You can get Line User Profile. See https://devdocs.line.me/en/#bot-api-get-profile for more detail.
```csharp
Profile profile = await lineClient.GetProflie("<LineMid>");
```

#### Reply Message
You can reply to Line Platform either by using reply or push. See https://devdocs.line.me/en/#reply-message for more detail.
```csharp
await lineClient.ReplyToActivityAsync(replyMessage);
```

#### Push Message. 
You can reply to Line Platform either by using reply or push. See https://devdocs.line.me/en/#push-message for more detail.
```csharp
await lineClient.PushAsync(replyMessage);
```

# Line Event
LineEvent class contains incoming message. See https://devdocs.line.me/en/#webhook-event-object for detail.

#### Create Reply
You can create reply easily from LineEvent.
```csharp
var reply = lineEvent.CreateReply(replyMessage);
```

# Create Reply Message
Following examples demonstrate how to construct reply message.
See https://devdocs.line.me/en/#send-message-object for detail of each message type.

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

#### Release Note
v1.2
  - Support Multicast
  - Add more comments
  - Bug fixes
v1.1 
  - Fix download content bug
v1.0
  - Initial Release
