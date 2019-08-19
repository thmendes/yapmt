<template>
    <div>
      <v-btn color="success" class="mr-1" depressed>
        <v-icon left>mdi-check-box-outline</v-icon>{{ completed }}
      </v-btn>
      <v-btn color="warning" class="mr-1" depressed>
        <v-icon left>mdi-alert-box-outline</v-icon>{{ late }}
      </v-btn>
      <v-btn depressed>
        <v-icon left>mdi-newspaper-variant-multiple-outline</v-icon>{{ total }}
      </v-btn>
    </div>
</template>

<script>
  import moment from 'moment';

  export default {
    props: {
        tasks: Array
    },
    data() {
        return {
          completed: 0,
          late: 0,
          total: 0,
        }
    },
    methods: {
      prepareStatus(){

        this.tasks.forEach(element => {
          if(element.completed)
            this.completed++;
          else if(element.dueDate < moment().format())
            this.late++;
        });
        
        this.total =  this.tasks.length;
      }
    },
    mounted() {
        this.prepareStatus();
    },
  }
</script>