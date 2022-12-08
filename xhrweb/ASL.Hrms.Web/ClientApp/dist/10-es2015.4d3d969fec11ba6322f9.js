(window.webpackJsonp=window.webpackJsonp||[]).push([[10],{jkDv:function(e,t,n){"use strict";n.r(t),n.d(t,"AdminModule",(function(){return x}));var l=n("tyNb"),s=n("PCNd"),o=n("40fR"),i=n("Vvy3"),a=n("Yj9t"),r=n("TwVa"),c=n("ZF+8"),d=n("Cmua"),m=n("fXoL"),p=n("3Pt+"),u=n("ofXK"),g=n("kmnG"),h=n("d3UM"),S=n("e/mZ"),f=n("FKr1");function v(e,t){1&e&&m["\u0275\u0275element"](0,"div",14)}function y(e,t){if(1&e&&(m["\u0275\u0275elementContainerStart"](0),m["\u0275\u0275elementStart"](1,"mat-option",15),m["\u0275\u0275text"](2),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementContainerEnd"]()),2&e){const e=t.$implicit;m["\u0275\u0275advance"](1),m["\u0275\u0275propertyInterpolate"]("value",e.id),m["\u0275\u0275advance"](1),m["\u0275\u0275textInterpolate"](e.fullName)}}function b(e,t){1&e&&(m["\u0275\u0275elementStart"](0,"span",16),m["\u0275\u0275text"](1," Please select an employee "),m["\u0275\u0275elementEnd"]())}function I(e,t){1&e&&(m["\u0275\u0275elementStart"](0,"span",16),m["\u0275\u0275text"](1," User Id not found "),m["\u0275\u0275elementEnd"]())}function A(e,t){1&e&&(m["\u0275\u0275elementStart"](0,"span",17),m["\u0275\u0275text"](1," Please select roles "),m["\u0275\u0275elementEnd"]())}const E=[{path:"assign-role",component:(()=>{class e{constructor(e,t,n,l,s){this.employeeService=e,this.alertService=t,this.companyService=n,this.formBuilder=l,this.roleManagementService=s,this.dropdownList=[],this.selectedItems=[],this.dropdownSettings={},this.loading=!1,this.submitted=!1,this.companyId=localStorage.getItem("companyId"),this.roleNames=[],this.userNotFound=!1}ngOnInit(){this.getAllCompanies(),this.getAllEmployees(),this.getAllRoles(),this.allColors=[{id:1,value:"red"},{id:2,value:"greed"}],this.dropdownList=[],this.selectedItems=[],this.dropdownSettings={singleSelection:!1,text:"Select Roles",selectAllText:"Select All",unSelectAllText:"UnSelect All",enableSearchFilter:!0,classes:"myclass custom-class"}}getAllEmployees(){this.loading=!0,this.employeeService.getAllEmployees().subscribe(e=>{this.employees=e,this.loading=!1},()=>{})}getAllCompanies(){this.companyService.getAllCompanies().subscribe(e=>{this.companies=e})}onSubmit(){this.submitted=!0,this.roleNames=this.selectedItems.map(e=>e.itemName),void 0!==this.employeeId&&0!==this.roleNames.length&&this.updateRoles()}onChangeRole(e){}getAllRoles(){this.loading=!0,this.roleManagementService.getRoles().subscribe(e=>{this.roles=e,this.loading=!1,this.addItemName(e)},()=>{this.loading=!1})}getRolesByEmployee(e){this.loading=!0,this.userNotFound=!1,this.submitted=!1,this.roleManagementService.getRolesByEmployee(e).subscribe(e=>{this.loading=!1,this.users=e,this.selectedItems=e},()=>{this.loading=!1})}onChangeEmployee(e){this.getRolesByEmployee(e.value),this.employeeId=e.value}onItemSelect(e){}onSelectAll(e){}addItemName(e){for(let t=0;t<e.length;t++)e[t].itemName=e[t].name;this.dropdownList=e}OnItemDeSelect(e){}onDeSelectAll(e){}updateRoles(){try{this.loading=!0;let{userId:e}=this.users.find(e=>e.id===this.employeeId);this.userRoles=new r.Db,this.userRoles.userId=e;let t=this.selectedItems.map(e=>e.itemName);this.userRoles.roles=t,this.roleManagementService.assignRoles(this.userRoles).subscribe(e=>{this.loading=!1,!0===e&&this.alertService.success("Roles assign success")},()=>{this.loading=!1})}catch(e){this.loading=!1,this.userNotFound=!0}}}return e.\u0275fac=function(t){return new(t||e)(m["\u0275\u0275directiveInject"](c.U),m["\u0275\u0275directiveInject"](d.a),m["\u0275\u0275directiveInject"](c.u),m["\u0275\u0275directiveInject"](p.g),m["\u0275\u0275directiveInject"](c.mb))},e.\u0275cmp=m["\u0275\u0275defineComponent"]({type:e,selectors:[["app-role-management"]],decls:18,vars:9,consts:[["class","spinner-border","style","position:fixed;z-index:9999;left:50%;top:30%; color:#218838;","role","status",4,"ngIf"],[1,"row","d-flex","justify-content-between","mt-3","mb-3"],[1,"col-sm-12","col-md-12"],["id","tableLabel"],[1,"row"],[1,"col-12","col-md-4","col-lg-3"],["appearance","outline"],["matInput","","placeholder","Select Employee",3,"value","valueChange","selectionChange"],[4,"ngFor","ngForOf"],["class","d-block text-danger error",4,"ngIf"],[1,"multi-select",3,"data","ngModel","settings","ngModelChange","onSelect","onDeSelect","onSelectAll","onDeSelectAll"],["class","d-block text-danger",4,"ngIf"],[1,"col-12","col-md-12","col-lg-3","p-0","mt-10"],["type","submit",1,"btn","btn-success",2,"font-size","smaller",3,"click"],["role","status",1,"spinner-border",2,"position","fixed","z-index","9999","left","50%","top","30%","color","#218838"],[3,"value"],[1,"d-block","text-danger","error"],[1,"d-block","text-danger"]],template:function(e,t){1&e&&(m["\u0275\u0275template"](0,v,1,0,"div",0),m["\u0275\u0275elementStart"](1,"div",1),m["\u0275\u0275elementStart"](2,"div",2),m["\u0275\u0275elementStart"](3,"h1",3),m["\u0275\u0275text"](4,"Assign Role"),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementStart"](5,"div",4),m["\u0275\u0275elementStart"](6,"div",5),m["\u0275\u0275elementStart"](7,"mat-form-field",6),m["\u0275\u0275elementStart"](8,"mat-select",7),m["\u0275\u0275listener"]("valueChange",(function(e){return t.employeeId=e}))("selectionChange",(function(e){return t.onChangeEmployee(e)})),m["\u0275\u0275template"](9,y,3,2,"ng-container",8),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementEnd"](),m["\u0275\u0275template"](10,b,2,0,"span",9),m["\u0275\u0275template"](11,I,2,0,"span",9),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementStart"](12,"div",5),m["\u0275\u0275elementStart"](13,"angular2-multiselect",10),m["\u0275\u0275listener"]("ngModelChange",(function(e){return t.selectedItems=e}))("onSelect",(function(e){return t.onItemSelect(e)}))("onDeSelect",(function(e){return t.OnItemDeSelect(e)}))("onSelectAll",(function(e){return t.onSelectAll(e)}))("onDeSelectAll",(function(e){return t.onDeSelectAll(e)})),m["\u0275\u0275elementEnd"](),m["\u0275\u0275template"](14,A,2,0,"span",11),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementStart"](15,"div",12),m["\u0275\u0275elementStart"](16,"button",13),m["\u0275\u0275listener"]("click",(function(){return t.onSubmit()})),m["\u0275\u0275text"](17," Save"),m["\u0275\u0275elementEnd"](),m["\u0275\u0275elementEnd"]()),2&e&&(m["\u0275\u0275property"]("ngIf",t.loading),m["\u0275\u0275advance"](8),m["\u0275\u0275property"]("value",t.employeeId),m["\u0275\u0275advance"](1),m["\u0275\u0275property"]("ngForOf",t.employees),m["\u0275\u0275advance"](1),m["\u0275\u0275property"]("ngIf",null==t.employeeId&&t.submitted),m["\u0275\u0275advance"](1),m["\u0275\u0275property"]("ngIf",t.userNotFound&&t.submitted),m["\u0275\u0275advance"](2),m["\u0275\u0275property"]("data",t.dropdownList)("ngModel",t.selectedItems)("settings",t.dropdownSettings),m["\u0275\u0275advance"](1),m["\u0275\u0275property"]("ngIf",0===t.roleNames.length&&t.submitted))},directives:[u.t,g.c,h.a,u.s,S.a,p.s,p.v,f.m],styles:[".c-btn{background-color:#fff;border:2px solid green}.multi-select[_ngcontent-%COMP%]{display:block;margin-top:4px}.error[_ngcontent-%COMP%]{margin-top:-23px}"]}),e})(),canActivate:[i.a],data:{roles:["Administrators"]}}];let x=(()=>{class e{}return e.\u0275mod=m["\u0275\u0275defineNgModule"]({type:e}),e.\u0275inj=m["\u0275\u0275defineInjector"]({factory:function(t){return new(t||e)},imports:[[s.a,a.AuthModule,o.a,S.b,l.g.forChild(E)]]}),e})()}}]);