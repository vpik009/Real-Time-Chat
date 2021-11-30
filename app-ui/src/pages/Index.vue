<template>
  <div class="row col-12 justify-center items-center full-height">
    <q-form
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
</div>
</template>

<script>
import { defineComponent } from 'vue';
import { HubConnectionBuilder, LogLevel} from "@aspnet/signalr";

export default defineComponent({
  data(){
    return{
      username: '',
      room: '',
      connection: null
    }
  },
  // mounted(){

  //   callHub.client.on()

  // },
  methods: {
    async onSubmit(user, room){

      // try{
        const client = new HubConnectionBuilder()
              .withUrl("https://localhost:5001/chat")
              .configureLogging(LogLevel.Information)
              .build();

        client.on("ReceiveMessage", (user, message) => {
          console.log("received message", message);
        })

        await client.start();
        await client.invoke("joinRoom", {user, room})

        this.connection = client;
      // }
      // catch(e){
      //   console.error(e);
      // }

    },
    onReset(){
        this.username = '';
        this.room = '';
    }
  }
})
</script>
