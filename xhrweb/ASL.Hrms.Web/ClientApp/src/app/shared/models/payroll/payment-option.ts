    export class PaymentOption {

          id :any; 
          paymentOptionId :any; 
          optionName :any; 
          optionCode :any; 
          sortOrder :any; 
          isDeleted :any; 

 public constructor (init?:Partial< PaymentOption > )
    {
        Object.assign(this,init);
    }
}



