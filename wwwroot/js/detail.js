const urlParams = new URLSearchParams(window.location.search);
const contentType = urlParams.get('content');
const contentId = urlParams.get("id");


var token = localStorage.getItem("token");
if(token == null){
    window.location.href = "Login.html";
}

const api = axios.create({
    baseURL: "https://localhost:44310/api/"
});
api.defaults.headers.common['Authorization'] = "Bearer " + token;

var app = new Vue({
    el: "#app",
    data: {
        content: [],
        genre:[],
        type: ""
    },
    mounted() {
        this.type = contentType;
        api.get(contentType + "/" + contentId)
            .then(response => {
                this.content = response.data;
                this.genre = response.data.genre;
            })
            .catch(error=>{
                if(error.response.data == "" && error.response.status==401){
                    localStorage.removeItem("token")
                    window.location.href = "Login.html";
                }
                console.log(error.response.data);   
            });
    },
    methods: {
    },
    filters: {
        ageFormat(stringDate) {
            return moment(stringDate).format('yyyy');
        },
        dateFormat(stringDate){
            moment.locale("es");
            return moment(stringDate).format("LL");
        }
    }
});