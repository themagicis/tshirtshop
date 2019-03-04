import { Urls } from '../config'

class HttpBackend{
    _userStore = null;

    constructor(userStore){
        this._userStore = userStore;
    }

    doPost(url, data){
        return this.doRequest("POST", url, data) 
    }

    doGet(url){
        return this.doRequest("GET", url) 
    }

    doRequest(type, url, data){
        return fetch(Urls.API + url, {
            method: type, 
            mode: "cors",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + this.getToken()
            },
            body: data && JSON.stringify(data)
        })
        .then(response => response.json())
        .catch(error => { 
            console.error(`Fetch Error =\n`, error)
            window.toastr.success('Unexpected error occured while sending data to the server. Please contact the support.');
        }); 
    }

    getToken(){
        return (this._userStore.getInfo() || {}).token;
    }
}

export default HttpBackend