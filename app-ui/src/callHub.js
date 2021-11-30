import { HubConnectionBuilder, LogLevel} from "@aspnet/signalr";

class CallHub{
    constructor(){
        this.client = new HubConnectionBuilder()
            .withUrl("https://localhost:8080/chat")
            .configureLogging(LogLevel.Information)
            .build();
  
        // this.client.on("ReceiveMessage", (user, message) => {
        //   console.log("message received " + message);
        // })
    }

    startsWith(){
        this.client.start();
    }
}

export default new CallHub();