import {} from './resolveurl';

export class BaseService{
    constructor(http,resolveUrl,addPath,loginPath){
        this.http = http;
        this.resolveUrl = resolveUrl;
        this.addPath = addPath;
        this.loginPath = loginPath;
    }
    getRoute(){
        return this.resolveUrl.getRoute(this.addPath);
    }
    getRouteLogin(){
        return this.resolveUrl.getRoute(this.loginPath);
    }
    getRouteById(id){
        return this.resolveUrl.getRouteById(this.path, id);
    } 
}
