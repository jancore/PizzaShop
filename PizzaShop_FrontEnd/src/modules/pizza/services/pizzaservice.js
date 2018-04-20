import { App } from '../../module';
import { BaseService } from '../../baseservice';

export class PizzaService extends BaseService {
    constructor(http, resolveUrl) {
        super(http, resolveUrl, 'pizzas/add','pizzas');
    }

    get(id) {
        return this.http.get(
            super.getRouteById(id)
        );
    }
    getAll() {
        return this.http.get(
            super.getSecondRoute()
        );
    }
    create(pizza) {        
        return this.http({
            method: 'POST',
            // set content-type to undefined so it is automatically selected
            headers: { 'Content-Type': undefined },
            url: super.getRoute(),
            data: {
                name: pizza.name,
                Ingredients: pizza.ingredients,
                File: pizza.img,
            },
            transformRequest: function (data, headersGetter) {
                let formData = new FormData();                
                angular.forEach(data, function (value, key) {
                    if (key === 'File' && value) {
                        formData.append(key, value, value.name);
                    }
                    if(key === "Ingredients"){
                        let index = 0;
                        angular.forEach(value, function(valor, keyingredient){                            
                            let ingredientsKey = key + '[' + index + ']'
                            formData.append(ingredientsKey, valor.Id);
                            index++;
                        });
                    }
                    else formData.append(key, value);
                });
                return formData;
            }
        });
        
    }
}
PizzaService.$inject = ['$http', 'resolveUrl'];
App.service('pizzaService', PizzaService);