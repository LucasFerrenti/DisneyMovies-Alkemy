const urlParams = new URLSearchParams(window.location.search);
const action = urlParams.get('action');
const activationToken = urlParams.get("token")

const api = axios.create({
    baseURL: "https://localhost:44310/api/",
});
var app = new Vue({
    el: "#app",
    data: {
        authRequest:{
            email:"",
            password:""
        },
        activationResult:"",
        error:[]
    },
    mounted(){
        if(action == "logout"){
            console.log(action)
            localStorage.removeItem("token");
        }
        else if (action =="activate"){
            api.defaults.headers.common['Authorization'] = "Bearer " + activationToken;
            console.log(api.defaults.headers.common['Authorization']);
            api.post("auth/confirm")
                .then(response => {
                    this.activationResult=response.data;
                    let toast = new bootstrap.Toast($("#liveToast"));
                    toast.show()
                })
                .catch(error=>{
                    this.error= error;
                    console.log(error.response.data)
                })
        }
    },
    methods: {
        login(){
            api.post("auth/login",this.authRequest)
                .then(response=>{
                    localStorage.setItem("token",response.data)
                    window.location.href="index.html"
                })
                .catch(error=>{
                    console.log(error.response.data);
                })
        },
        register(){
            api.post("auth/register",this.authRequest)
                .then(response=>{
                    console.log(response.data)
                })
                .catch(error=>{
                    console.log(error.response.data);
                })
        },
    },
    filters: {
        ageFormat(stringDate) {
            return moment(stringDate).format('yyyy');
        }
    }
});