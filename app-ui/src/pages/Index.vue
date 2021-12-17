<template>
  <div class="row col-12 justify-center">
    <q-form v-if="!connection"
      @submit="onSubmit(username, room)"
      @reset="onReset"
      class="q-gutter-md col-12"
    >
      <div class="q-pa-md row justify-center col-12 items-center">
        <q-input outlined v-model="username" label="Username" class="col-6 q-mb-lg q-mt-xl"/>
      </div>
      <div class="q-pa-md row justify-center col-12 items-center">
        <q-input outlined v-model="room" label="Room" class="col-6 q-mb-lg q-mb-lg"/>
      </div>
      <div class="q-pa-md row justify-center col-12 items-center">
        <q-btn v-if="!username||!room" color="primary" label="JOIN" disable/>
        <q-btn v-else color="primary" type="submit" label="JOIN" />
        <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
      </div>
    </q-form>
    <!--Connection established-->
    <q-form v-else class="col-12 q-mt-xl" @submit="sendMessage(username, curMessage)"> 
      <div class="row justify-center">
        <div
        class="row"
        style="width: 100%; max-width: 400px;">
          <div v-for='msg in messages' :key="msg" class="col-12">
            <q-chat-message v-if="msg.user === username"
              :text="[msg.message]"
              :name="msg.user"
              sent
            />
            <q-chat-message v-else
              :text="[msg.message]"
              :name="msg.user"
              
            />
          </div>
        </div>
        <div class="q-pa-md row justify-center col-12 items-center q-mt-xl">
          <q-input outlined v-model="curMessage" label="message..." class="col-7 q-ml-md" dense/>
          <q-btn v-if="!curMessage" color="primary" label="Send" type="submit" class="col-2 q-mr-md" disable/>
          <q-btn v-else color="primary" label="Send" type="submit" class="col-2 q-mr-md"/>
        </div>
      </div>

    </q-form>
</div>
</template>

<script>
import { defineComponent } from 'vue';
import { HubConnectionBuilder, LogLevel} from "@microsoft/signalr";


export default defineComponent({

  data(){
    return{
      username: '',
      room: '',
      connection: null,
      messages: [],
      curMessage: ''
    }
  },

  methods: {
    async onSubmit(user, room){

      try{
        const client = new HubConnectionBuilder()
              .withUrl("https://localhost:5001/chat")
              .configureLogging(LogLevel.Information)
              .build();

        client.on("ReceiveMessage", (user, message) => {
          this.messages = [...this.messages, {user, message}];
        })

        this.connection = client;

        await client.start();
        await client.invoke("joinRoom", {user, room})

        
        
      }
      catch(e){
        console.error(e);
      }

      

    },
    onReset(){
        this.username = '';
        this.room = '';
    },
    async sendMessage(user, message){
      try{
 
        await this.connection.invoke("sendMessage", message);

        this.curMessage = '';
      }
      catch(e){
        console.error(e);
      }
    }
  }
})
</script>
