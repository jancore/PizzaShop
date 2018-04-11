import { App } from '../../module';
import { BaseService } from '../../baseservice';

export class UserService extends BaseService {
    constructor(http, resolveUrl) {
        super(http, resolveUrl, 'api/Account/Register');
    }

    get(id) {
        return this.http.get(
            super.getRouteById(id)
        );
    }
    getAll() {
        return this.http.get(
            super.getRoute()
        );
    }
    create(user) {
        // in the development database (json-server)
        // we do not store the images
        if(user.img) delete user.img;
        return this.http.post(
            super.getRoute(),
            user
        );
        /* 
        $http({
            method: 'POST',
            url: 'http://localhost:51889/api/Account/Register',
            data: JSON.stringify(this.user)
        });
        */
    }
}
UserService.$inject = ['$http', 'resolveUrl'];
App.service('userService', UserService);