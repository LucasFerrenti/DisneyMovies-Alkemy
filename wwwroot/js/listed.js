const urlParams = new URLSearchParams(window.location.search);
var contentType = urlParams.get('content');

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
        type: ""
    },
    mounted() {
        if (contentType != "characters") {
            this.type = "movies";
        }
        else {
            this.type = contentType;
        }
        $("." + this.type).addClass("active");
        
        api.get(this.type)
            .then(response => {
                this.content = response.data;
            })
            .catch(error => {
                if(error.response.data == "" && error.response.status==401){
                    localStorage.removeItem("token")
                    window.location.href = "Login.html";
                }
                console.log(error.response.data);
            });
    },
    filters: {
        ageFormat(stringDate) {
            return moment(stringDate).format('yyyy');
        }
    }
});