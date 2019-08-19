<template>
  <v-container>
    <v-row>
      <v-col cols="12" sm="12">
        <v-card outlined>
            <v-card-title
              v-if="this.project.id != ''"
              primary-title
            >
            {{ project.name }}
            <v-spacer></v-spacer>
            <status :tasks="tasks" :key="statusKey"></status>
            </v-card-title>
            <v-card-title 
              v-else
            >
            <v-text-field
                v-model="project.name"
                label="Type new project name"
                v-on:keyup.enter="createProject"
                required
                :rules="[rules.nameRequired]"
              ></v-text-field> 
            </v-card-title>
          <v-card-text>
            <Task />
          </v-card-text>
          <v-card-actions v-if="this.project.id != ''">
            <v-spacer></v-spacer>
            <v-dialog
              v-model="dialog"
              width="550"
            >
              <template v-slot:activator="{ on }">
                <v-btn
                  color="red lighten-2"
                  dark
                  v-on="on"
                >
                  delete
                </v-btn>
              </template>

              <v-card>
                <v-card-title
                  class="headline grey lighten-2"
                  primary-title
                >
                  Are you sure you want to delete this project?
                </v-card-title>

                <v-divider></v-divider>

                <v-card-actions>
                  <v-btn
                    color="primary"
                    @click="dialog = false"
                  >
                    Calcel
                  </v-btn>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="primary"
                    text
                    @click="deleteProject"
                  >
                    Yes, delete this.
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  
  import axios from 'axios';
  import bus from '../bus';
  import Task from './Task';
  import status from './Status'

  const ownerRequired = (value) => value.indexOf('@') >= 0;

  export default {
    components:{
      Task,
      status
    },
    data() {
      return {
        rules: {
          nameRequired: value => !!value || 'Name is required'
        },
        project: {
          id: '',
          name: '',
          tasks: [] 
        },
        dialog: false,
        statusKey: 0,
        tasks: []
      }
    },
    methods: {
      fetchProject(projectId){
        axios.get('api/project/' + projectId) 
        .then((response) => {
          this.project = response.data;
          this.loadProject(response.data);
        });
      },
      fetchProjects() {
        axios.get('api/project')
        .then((response) => {
          if(response.data.length > 0){
            var projects = response.data;
            this.project = projects[0];
            this.refreshProjects(projects);
            this.loadProject(projects[0]);
          }
          else{
            this.prepareNewProject();
            this.refreshProjects(response.data);
          }
        });
      },
      createProject(){
        if(this.project.Name != '') 
        {
          axios.post('/api/project', {
            'Name': this.project.name,
            'Tasks': this.project.tasks
          })
          .then((response) =>{
            this.fetchProjects();
          });
        }
        else{
          this.$refs.form.validate();          
        }
      },
      deleteProject(){
        axios.delete('/api/project/' + this.project.id)
        .then(() => {
          this.dialog = false;

          this.fetchProjects();
        })
      },
      prepareNewProject(){
        this.project = {
          id: '',
          name: '',
          tasks: [] 
        };
        this.loadProject(this.project);
      },
      refreshProjects(projects){
        bus.$emit('refreshProjects', projects);
      },
      loadProject(project){
        bus.$emit('loadProject', project);
      },
      listenToEvents(){
        bus.$on('newProject', () => {
          this.prepareNewProject();
        });

        bus.$on('selectedProject', (projectId) => {
          this.fetchProject(projectId);
        });

        bus.$on('syncTasksStatus', (response) => {
          this.tasks = response;
          this.statusKey++;
        });
      }
    },
    created() {
      this.listenToEvents();
      this.fetchProjects();
    },
  }
</script>

