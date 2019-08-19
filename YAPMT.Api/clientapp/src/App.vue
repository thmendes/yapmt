<template>
  <v-app>
    <v-navigation-drawer persistent :mini-variant="miniVariant" :clipped="clipped" v-model="drawer" enable-resize-watcher fixed app >
      <v-list>
        <v-list-item-group color="primary">
          <v-subheader>Projects</v-subheader>
          <v-list-item v-for="(project, i) in projects" :key="i" @click="selectedProject(project.id)">
              <v-list-item-icon>
                <v-icon>mdi-newspaper</v-icon>
              </v-list-item-icon>
              <v-list-item-content>
                <v-list-item-title v-text="project.name"></v-list-item-title>
              </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-btn 
                @click="newProject"
                color="primary"
              >
                <v-icon left>mdi-plus</v-icon> Create new Project
              </v-btn>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar
      app
      clipped-left
      color="white"
      dense
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title class="mr-12 align-center">
        <span class="title">YAPMT</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-layout
        align-center
        style="max-width: 650px"
      >
      </v-layout>
    </v-app-bar>

    <v-content>
      <Project />
    </v-content>
  </v-app>
</template>

<script>
import Project from './components/Project';
import axios from 'axios';
import bus from './bus';

export default {
  name: 'App',
  components: {
    Project,
  },
  data: () => ({
    clipped: true,
    drawer: true,
    miniVariant: false,
    right: true,
    projects: []
  }),
  methods: {
    listenToEvents(){
      bus.$on('refreshProjects', (response) => {
        this.projects = response;
      });
    },
    newProject(){
      bus.$emit('newProject');
    },
    selectedProject(id){
      bus.$emit('selectedProject', id);
    }
  },
  created() {
    this.listenToEvents();
  }
}
</script>