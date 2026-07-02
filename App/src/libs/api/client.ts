import axios from "axios";


const apiClient = axios.create({
    baseURL: "",
    timeout: 10000,
    headers: {}
})

//will be used to retirive the token and use it
apiClient.interceptors.request.use(async (config) =>{
    config.headers.Authorization = ""
   return config
});

//Will be used to resend auth token on expiry
apiClient.interceptors.response.use(async (error) => {


    

    return error
})




export default apiClient