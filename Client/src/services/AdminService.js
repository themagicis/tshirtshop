class AdminService {
    constructor(http){
        this.http = http;
    }

    getAttributes(){
        return this.http.doGet('attributes');
    }

    getAttributeValues(name){
        return this.http.doGet(`attributes/${name}/values`);
    }

    getDepartments(){
        return this.http.doGet('departments');
    }

    getCategories(name){
        return this.http.doGet(`departments/${name}/categories`);
    }
}

export default AdminService