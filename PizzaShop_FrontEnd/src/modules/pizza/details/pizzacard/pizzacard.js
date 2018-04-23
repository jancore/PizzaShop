import { App } from '../../../module';
import html from './pizzacard.html';

export const ilPizzaCard = {
  template: html,
  bindings:{
    name:"@",
    URL:"@"
  }

}
App.component('ilPizzaCard', ilPizzaCard);