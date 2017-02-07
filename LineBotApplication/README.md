# LineBotApplition
Visual Studio 2015 project template for Line Messaging API.

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
4. Update ChannelSecret and ChannelToken in Web.Config file. <br/>
You can get there values from https://business.line.me

# Template features
This templates does followings.

### Varidate Signature
When receiving Post message from Line Platform, it verifies the request by following the documentation at https://devdocs.line.me/en/#signature-validation

### Parse and handle Incoming message
Parse the incoming Post body and handle event. LineMessageHandler class contains methods to handle each type.

### Create reply message sample
Several types such as Text, Location or Media type of message already has sample reply implemented.

### Reply/Push message
Finally, it replies to Line Platform by using either reply or push. Find detail information here.

Reply Message: https://devdocs.line.me/en/#reply-message <br/>
Push Message: https://devdocs.line.me/en/#push-message

### 日本語の記事 ###
以下記事に使い方の例が非常にわかりやすく掲載されていますので、是非ご覧ください。
LUISを使って頭の悪いLINE Botを作ってみよう！
http://www.atmarkit.co.jp/ait/articles/1702/03/news026.html

