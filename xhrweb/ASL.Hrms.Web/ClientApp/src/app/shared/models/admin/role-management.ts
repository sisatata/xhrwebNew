export class UserRoleDto {
    id: any;
    userId: any;

    roles:string[];
    public constructor(init?: Partial<UserRoleDto>) {
        Object.assign(this, init);
    }

}
