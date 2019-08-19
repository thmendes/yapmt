<template>
  <v-form 
    ref="form"
    lazy-validation
    v-if="this.project.id != ''"
  >
    <template v-for="item in project.tasks">
      <v-checkbox 
        v-bind:key="item.id" 
        v-model="item.completed"
        @click.native="updateTask(item)"
      >
        <span slot="label" v-bind:item="item">
          <fancytask :task="item" :key="item.completed"></fancytask>
        </span>
      </v-checkbox>
    </template>
    <v-checkbox
      style="float:left"
      v-model="unprocessedTask.Completed"
      hide-details
    ></v-checkbox>
    <v-text-field 
      v-model="unprocessedTask.Description" 
      :rules="[rules.owner]"
      label="type new task, @owner, M/d"
    ></v-text-field>
    <div class="my-2">
      <v-btn 
        small 
        color="primary" 
        @click="createTask" 
        dark
      >
        <v-icon left>mdi-plus</v-icon> Add Task
      </v-btn>
    </div>
  </v-form>
  <v-form 
    ref="form"
    lazy-validation
    v-else
  >
    <template v-for="item in project.tasks">
     <v-checkbox 
        v-bind:key="item.id" 
        v-model="item.completed"
      >
        <span slot="label" v-bind:item="item">
          <fancytask :task="item" :key="item.completed"></fancytask>
        </span>
      </v-checkbox>
    </template>
    <v-checkbox
      style="float:left"
      v-model="unprocessedTask.Completed"
      hide-details
    ></v-checkbox>
    <v-text-field 
      v-model="unprocessedTask.Description" 
      :rules="[rules.owner]"
      label="type new task, @owner, M/d"
    ></v-text-field>
    <div class="my-2">
      <v-btn 
        small 
        color="primary" 
        @click="addTask" 
        dark
      >
        <v-icon left>mdi-plus</v-icon> Add Task</v-btn>
    </div>
  </v-form>
</template>

<script>
  
  import bus from '../bus';
  import fancytask from './FancyTask'
  import uud from 'uuid/v4';
  import moment from 'moment';
  import axios from 'axios';

  export default {
    components: {
      fancytask
    },
    data() {
      return {
        taskProject: '',
        project: {
          id: '',
          name: '',
          tasks: [],
        },
        unprocessedTask: {
          Description: '',
          Completed: false
        },
        rules: {
          owner: value => !value || (value.indexOf('@') >= 0 && (value.match(/,/g) || []).length > 0 && (value.match(/\b([1-9]|10|11|12)\/([1-9]|[1-2][0-9]|3[0-1])\b$/) || []).length > 0)  || 'Task must follow the above schema',
        }
      }
    },
    methods: {
      addTask (){
        if(this.unprocessedTask.Description != ''){
          var task = this.prepareTask(this.unprocessedTask);
          this.project.tasks.push(task);
          this.resetForm();
        }
      },
      prepareTask(unprocessedTask){
        var strArray = unprocessedTask.Description.split('@');
        var strSubArray = strArray[1].split(',');

        var task = {
          id: uud(),
          description: strArray[0].trim().slice(0, -1),
          owner: '@' + strSubArray[0].trim(),
          dueDate: moment(strSubArray[1].trim(), 'MM-DD'),
          completed: unprocessedTask.Completed
        };

        return task;
      },
      updateTask(task){
        axios.put('/api/project/' + this.project.id + '/task/', task)
          .then((response) =>{
             this.project = response.data;
             this.syncTasksStatus();
        });
      },
      createTask(){
        var task = this.prepareTask(this.unprocessedTask);
        axios.post('/api/project/' + this.project.id + '/task', task)
          .then((response) =>{
            this.project = response.data;
            this.resetForm();
          });
      },
      syncTasksStatus(){
        bus.$emit('syncTasksStatus', this.project.tasks);
      },
      listenToEvents(){
        bus.$on('loadProject', (response) => {
          this.project = response;
          this.syncTasksStatus();
        });
      },
      resetForm(){
        this.unprocessedTask.Description = '';
        this.unprocessedTask.Completed = false; 
        this.$refs.form.resetValidation();
      }
    },
    created() {
      this.listenToEvents();
    }
  }
</script>