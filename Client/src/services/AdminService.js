class AdminService {
    constructor(http){
        this.http = http;
    }

    getReports(){
        return this.http.doGet('admin/getReports');
    }
}

export default AdminService