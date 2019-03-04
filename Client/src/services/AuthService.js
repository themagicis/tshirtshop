class AuthService  {
    constructor(http){
        this.http = http;
    }

    login(email, password){
        return this.http.doPost('auth/login', {email, password});
    }

    register(name, email, password, picture){
        return this.http.doPost('auth/signup', {name, email, password, picture});
    }
}

export default AuthService;