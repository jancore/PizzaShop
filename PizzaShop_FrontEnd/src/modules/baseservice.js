import {} from './resolveurl';

export class BaseService{
    constructor(http,resolveUrl,path){
        this.http = http;
        this.resolveUrl = resolveUrl;
        this.path = path;
    }
    getRoute(){
        return this.resolveUrl.getRoute(this.path);
    }
    getRouteById(id){
        return this.resolveUrl.getRouteById(this.path, id);
    } 
}
