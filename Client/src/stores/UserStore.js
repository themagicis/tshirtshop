import { observable, computed, action } from "mobx"

let defaultInfo = {
    id: 0,
    token: '',
    name: '',
    roles: [],
}

class UserStore {
    static key = '__user__';
    constructor(){
        var info = localStorage.getItem(UserStore.key);
        if (info){
            this.setInfo(JSON.parse(info));
        } else{
            this.setInfo(defaultInfo);
        }
    }

    @observable info = {
        id: 0,
        token: '',
        name: '',
        roles: []
    };

    @action setInfo = info => {
        Object.assign(this.info, info);
        localStorage.setItem(UserStore.key, JSON.stringify(this.info));
    }

    @action clearInfo = () => {
        Object.assign(this.info, defaultInfo);
        localStorage.removeItem(UserStore.key);
    }

    getInfo = () =>{
        var info = localStorage.getItem(UserStore.key);
        if (info){
            return JSON.parse(info)
        }
        return null;
    }

    @computed get isAuthenticated() {
        return this.info.id !== 0;
    }

    @computed get isAdmin(){
        return this.info.roles.indexOf('Admin') >= 0;
    }
}

export default UserStore;