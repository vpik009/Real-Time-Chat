<template>
  <div class="q-pa-md row justify-center">
    <div style="width: 100%; max-width: 400px">
      <q-chat-message v-for="msg in messageYou" :key="msg"
        :text="[msg]"
        sent
      />
      <q-chat-message v-for="msg in messageNotYou" :key="msg"
        :text="[msg]"
      />
    </div>
  </div>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent({
  data(){
    return{
      messageYou: [
        'hey how r u?',
        "Crazy"
      ],
      messageNotYou: [
        'im good'
      ]
    }
  }
})
</script>
