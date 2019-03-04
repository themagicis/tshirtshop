import AdminService from '../services/AdminService'
import AuthService from '../services/AuthService'

class ApiStore {
    constructor(http){
        this.admin = new AdminService(http)
        this.auth = new AuthService(http);
    }
}

export default ApiStore;