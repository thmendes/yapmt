<template>
    <div>
        <span v-html="description"></span>
    </div>
</template>

<script>

import moment from 'moment';

export default {
    props: {
        task: Object
    },
    data() {
        return {
            description: ''
        }
    },
    methods:{
        prepareDescrition(){

            if(this.task.completed) 
            {
                this.description = '<strike>' + this.task.description + ' <b>' + this.task.owner + '</b> ' + moment(this.task.dueDate).fromNow() + '</strike>';    
            }
            else if(this.task.dueDate < moment().format())
            {
                this.description = '<span class="red--text">' + this.task.description + ' <b>' + this.task.owner + '</b> ' + moment(this.task.dueDate).fromNow() + '</span>';
            }
            else{
                this.description = this.task.description + ' <b>' + this.task.owner + '</b> ' + moment(this.task.dueDate).fromNow();
            }
        }
    },
    mounted() {
        this.prepareDescrition();
    },
}
</script>