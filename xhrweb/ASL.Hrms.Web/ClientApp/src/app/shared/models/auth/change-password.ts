export class ChangePassword {
    email:string='';
    currentPassword: string ='';
    newPassword:string = ''; 
    public constructor (init?:Partial<ChangePassword>){
        Object.assign(this,init);
    }

}
