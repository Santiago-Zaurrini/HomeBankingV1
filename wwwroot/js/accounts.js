var app = new Vue({
    el:"#app",
    data:{
        clientInfo: {},
        error: null
    },
    methods:{
        getData: function () {
            const urlParams = new URLSearchParams(window.location.search);
            const id = urlParams.get('id');
            axios.get(`/api/clients/${id}`)
            .then(function (response) {
                //get client ifo
                app.clientInfo = response.data;
            })
            .catch(function (error) {
                // handle error
                app.error = error;
            })
        },
        formatDate: function(date){
            return new Date(date).toLocaleDateString('en-gb');
        }
    },
    mounted: function(){
        this.getData();
    }
})