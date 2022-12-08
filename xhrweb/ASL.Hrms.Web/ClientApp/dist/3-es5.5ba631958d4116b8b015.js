!function(){function e(t,n,i){return(e="undefined"!=typeof Reflect&&Reflect.get?Reflect.get:function(e,t,n){var i=function(e,t){for(;!Object.prototype.hasOwnProperty.call(e,t)&&null!==(e=a(e)););return e}(e,t);if(i){var o=Object.getOwnPropertyDescriptor(i,t);return o.get?o.get.call(n):o.value}})(t,n,i||t)}function t(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function");e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,writable:!0,configurable:!0}}),t&&n(e,t)}function n(e,t){return(n=Object.setPrototypeOf||function(e,t){return e.__proto__=t,e})(e,t)}function i(e){var t=function(){if("undefined"==typeof Reflect||!Reflect.construct)return!1;if(Reflect.construct.sham)return!1;if("function"==typeof Proxy)return!0;try{return Date.prototype.toString.call(Reflect.construct(Date,[],(function(){}))),!0}catch(e){return!1}}();return function(){var n,i=a(e);if(t){var r=a(this).constructor;n=Reflect.construct(i,arguments,r)}else n=i.apply(this,arguments);return o(this,n)}}function o(e,t){return!t||"object"!=typeof t&&"function"!=typeof t?function(e){if(void 0===e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return e}(e):t}function a(e){return(a=Object.setPrototypeOf?Object.getPrototypeOf:function(e){return e.__proto__||Object.getPrototypeOf(e)})(e)}function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function s(e,t){for(var n=0;n<t.length;n++){var i=t[n];i.enumerable=i.enumerable||!1,i.configurable=!0,"value"in i&&(i.writable=!0),Object.defineProperty(e,i.key,i)}}function c(e,t,n){return t&&s(e.prototype,t),n&&s(e,n),e}(window.webpackJsonp=window.webpackJsonp||[]).push([[3],{"7EHt":function(n,o,s){"use strict";s.d(o,"a",(function(){return ae})),s.d(o,"b",(function(){return se})),s.d(o,"c",(function(){return ee})),s.d(o,"d",(function(){return te})),s.d(o,"e",(function(){return Z})),s.d(o,"f",(function(){return ie})),s.d(o,"g",(function(){return ne})),s.d(o,"h",(function(){return oe}));var d,l,p,u=s("fXoL"),h=s("8LU1"),f=s("XNiG"),g=s("quSY"),m=s("0EQZ"),x=0,b=((p=function(){function e(){r(this,e),this._stateChanges=new f.a,this._openCloseAllActions=new f.a,this.id="cdk-accordion-"+x++,this._multi=!1}return c(e,[{key:"openAll",value:function(){this._openCloseAll(!0)}},{key:"closeAll",value:function(){this._openCloseAll(!1)}},{key:"ngOnChanges",value:function(e){this._stateChanges.next(e)}},{key:"ngOnDestroy",value:function(){this._stateChanges.complete()}},{key:"_openCloseAll",value:function(e){this.multi&&this._openCloseAllActions.next(e)}},{key:"multi",get:function(){return this._multi},set:function(e){this._multi=Object(h.c)(e)}}]),e}()).\u0275fac=function(e){return new(e||p)},p.\u0275dir=u["\u0275\u0275defineDirective"]({type:p,selectors:[["cdk-accordion"],["","cdkAccordion",""]],inputs:{multi:"multi"},exportAs:["cdkAccordion"],features:[u["\u0275\u0275NgOnChangesFeature"]]}),p),y=0,v=((l=function(){function e(t,n,i){var o=this;r(this,e),this.accordion=t,this._changeDetectorRef=n,this._expansionDispatcher=i,this._openCloseAllSubscription=g.a.EMPTY,this.closed=new u.EventEmitter,this.opened=new u.EventEmitter,this.destroyed=new u.EventEmitter,this.expandedChange=new u.EventEmitter,this.id="cdk-accordion-child-"+y++,this._expanded=!1,this._disabled=!1,this._removeUniqueSelectionListener=function(){},this._removeUniqueSelectionListener=i.listen((function(e,t){o.accordion&&!o.accordion.multi&&o.accordion.id===t&&o.id!==e&&(o.expanded=!1)})),this.accordion&&(this._openCloseAllSubscription=this._subscribeToOpenCloseAllActions())}return c(e,[{key:"ngOnDestroy",value:function(){this.opened.complete(),this.closed.complete(),this.destroyed.emit(),this.destroyed.complete(),this._removeUniqueSelectionListener(),this._openCloseAllSubscription.unsubscribe()}},{key:"toggle",value:function(){this.disabled||(this.expanded=!this.expanded)}},{key:"close",value:function(){this.disabled||(this.expanded=!1)}},{key:"open",value:function(){this.disabled||(this.expanded=!0)}},{key:"_subscribeToOpenCloseAllActions",value:function(){var e=this;return this.accordion._openCloseAllActions.subscribe((function(t){e.disabled||(e.expanded=t)}))}},{key:"expanded",get:function(){return this._expanded},set:function(e){e=Object(h.c)(e),this._expanded!==e&&(this._expanded=e,this.expandedChange.emit(e),e?(this.opened.emit(),this._expansionDispatcher.notify(this.id,this.accordion?this.accordion.id:this.id)):this.closed.emit(),this._changeDetectorRef.markForCheck())}},{key:"disabled",get:function(){return this._disabled},set:function(e){this._disabled=Object(h.c)(e)}}]),e}()).\u0275fac=function(e){return new(e||l)(u["\u0275\u0275directiveInject"](b,12),u["\u0275\u0275directiveInject"](u.ChangeDetectorRef),u["\u0275\u0275directiveInject"](m.d))},l.\u0275dir=u["\u0275\u0275defineDirective"]({type:l,selectors:[["cdk-accordion-item"],["","cdkAccordionItem",""]],inputs:{expanded:"expanded",disabled:"disabled"},outputs:{closed:"closed",opened:"opened",destroyed:"destroyed",expandedChange:"expandedChange"},exportAs:["cdkAccordionItem"],features:[u["\u0275\u0275ProvidersFeature"]([{provide:b,useValue:void 0}])]}),l),_=((d=function e(){r(this,e)}).\u0275mod=u["\u0275\u0275defineNgModule"]({type:d}),d.\u0275inj=u["\u0275\u0275defineInjector"]({factory:function(e){return new(e||d)}}),d),w=s("+rOU"),k=s("ofXK"),j=s("u47x"),O=s("FtGj"),C=s("/uUt"),A=s("JX91"),E=s("pLZG"),I=s("IzEk"),P=s("R1ws"),D=s("EY2u"),T=s("VRyK"),S=s("R0Ic"),R=["body"];function H(e,t){}var M=[[["mat-expansion-panel-header"]],"*",[["mat-action-row"]]],F=["mat-expansion-panel-header","*","mat-action-row"];function N(e,t){if(1&e&&u["\u0275\u0275element"](0,"span",2),2&e){var n=u["\u0275\u0275nextContext"]();u["\u0275\u0275property"]("@indicatorRotate",n._getExpandedState())}}var Q,z,L,V,q,U,K,X,Y=[[["mat-panel-title"]],[["mat-panel-description"]],"*"],B=["mat-panel-title","mat-panel-description","*"],G=new u.InjectionToken("MAT_ACCORDION"),J={indicatorRotate:Object(S.m)("indicatorRotate",[Object(S.j)("collapsed, void",Object(S.k)({transform:"rotate(0deg)"})),Object(S.j)("expanded",Object(S.k)({transform:"rotate(180deg)"})),Object(S.l)("expanded <=> collapsed, void => collapsed",Object(S.e)("225ms cubic-bezier(0.4,0.0,0.2,1)"))]),bodyExpansion:Object(S.m)("bodyExpansion",[Object(S.j)("collapsed, void",Object(S.k)({height:"0px",visibility:"hidden"})),Object(S.j)("expanded",Object(S.k)({height:"*",visibility:"visible"})),Object(S.l)("expanded <=> collapsed, void => collapsed",Object(S.e)("225ms cubic-bezier(0.4,0.0,0.2,1)"))])},Z=((Q=function e(t){r(this,e),this._template=t}).\u0275fac=function(e){return new(e||Q)(u["\u0275\u0275directiveInject"](u.TemplateRef))},Q.\u0275dir=u["\u0275\u0275defineDirective"]({type:Q,selectors:[["ng-template","matExpansionPanelContent",""]]}),Q),W=0,$=new u.InjectionToken("MAT_EXPANSION_PANEL_DEFAULT_OPTIONS"),ee=((K=function(n){t(s,n);var o=i(s);function s(e,t,n,i,a,c,d){var l;return r(this,s),(l=o.call(this,e,t,n))._viewContainerRef=i,l._animationMode=c,l._hideToggle=!1,l.afterExpand=new u.EventEmitter,l.afterCollapse=new u.EventEmitter,l._inputChanges=new f.a,l._headerId="mat-expansion-panel-header-"+W++,l._bodyAnimationDone=new f.a,l.accordion=e,l._document=a,l._bodyAnimationDone.pipe(Object(C.a)((function(e,t){return e.fromState===t.fromState&&e.toState===t.toState}))).subscribe((function(e){"void"!==e.fromState&&("expanded"===e.toState?l.afterExpand.emit():"collapsed"===e.toState&&l.afterCollapse.emit())})),d&&(l.hideToggle=d.hideToggle),l}return c(s,[{key:"_hasSpacing",value:function(){return!!this.accordion&&this.expanded&&"default"===this.accordion.displayMode}},{key:"_getExpandedState",value:function(){return this.expanded?"expanded":"collapsed"}},{key:"toggle",value:function(){this.expanded=!this.expanded}},{key:"close",value:function(){this.expanded=!1}},{key:"open",value:function(){this.expanded=!0}},{key:"ngAfterContentInit",value:function(){var e=this;this._lazyContent&&this.opened.pipe(Object(A.a)(null),Object(E.a)((function(){return e.expanded&&!e._portal})),Object(I.a)(1)).subscribe((function(){e._portal=new w.j(e._lazyContent._template,e._viewContainerRef)}))}},{key:"ngOnChanges",value:function(e){this._inputChanges.next(e)}},{key:"ngOnDestroy",value:function(){e(a(s.prototype),"ngOnDestroy",this).call(this),this._bodyAnimationDone.complete(),this._inputChanges.complete()}},{key:"_containsFocus",value:function(){if(this._body){var e=this._document.activeElement,t=this._body.nativeElement;return e===t||t.contains(e)}return!1}},{key:"hideToggle",get:function(){return this._hideToggle||this.accordion&&this.accordion.hideToggle},set:function(e){this._hideToggle=Object(h.c)(e)}},{key:"togglePosition",get:function(){return this._togglePosition||this.accordion&&this.accordion.togglePosition},set:function(e){this._togglePosition=e}}]),s}(v)).\u0275fac=function(e){return new(e||K)(u["\u0275\u0275directiveInject"](G,12),u["\u0275\u0275directiveInject"](u.ChangeDetectorRef),u["\u0275\u0275directiveInject"](m.d),u["\u0275\u0275directiveInject"](u.ViewContainerRef),u["\u0275\u0275directiveInject"](k.e),u["\u0275\u0275directiveInject"](P.a,8),u["\u0275\u0275directiveInject"]($,8))},K.\u0275cmp=u["\u0275\u0275defineComponent"]({type:K,selectors:[["mat-expansion-panel"]],contentQueries:function(e,t,n){var i;1&e&&u["\u0275\u0275contentQuery"](n,Z,!0),2&e&&u["\u0275\u0275queryRefresh"](i=u["\u0275\u0275loadQuery"]())&&(t._lazyContent=i.first)},viewQuery:function(e,t){var n;1&e&&u["\u0275\u0275viewQuery"](R,!0),2&e&&u["\u0275\u0275queryRefresh"](n=u["\u0275\u0275loadQuery"]())&&(t._body=n.first)},hostAttrs:[1,"mat-expansion-panel"],hostVars:6,hostBindings:function(e,t){2&e&&u["\u0275\u0275classProp"]("mat-expanded",t.expanded)("_mat-animation-noopable","NoopAnimations"===t._animationMode)("mat-expansion-panel-spacing",t._hasSpacing())},inputs:{disabled:"disabled",expanded:"expanded",hideToggle:"hideToggle",togglePosition:"togglePosition"},outputs:{opened:"opened",closed:"closed",expandedChange:"expandedChange",afterExpand:"afterExpand",afterCollapse:"afterCollapse"},exportAs:["matExpansionPanel"],features:[u["\u0275\u0275ProvidersFeature"]([{provide:G,useValue:void 0}]),u["\u0275\u0275InheritDefinitionFeature"],u["\u0275\u0275NgOnChangesFeature"]],ngContentSelectors:F,decls:7,vars:4,consts:[["role","region",1,"mat-expansion-panel-content",3,"id"],["body",""],[1,"mat-expansion-panel-body"],[3,"cdkPortalOutlet"]],template:function(e,t){1&e&&(u["\u0275\u0275projectionDef"](M),u["\u0275\u0275projection"](0),u["\u0275\u0275elementStart"](1,"div",0,1),u["\u0275\u0275listener"]("@bodyExpansion.done",(function(e){return t._bodyAnimationDone.next(e)})),u["\u0275\u0275elementStart"](3,"div",2),u["\u0275\u0275projection"](4,1),u["\u0275\u0275template"](5,H,0,0,"ng-template",3),u["\u0275\u0275elementEnd"](),u["\u0275\u0275projection"](6,2),u["\u0275\u0275elementEnd"]()),2&e&&(u["\u0275\u0275advance"](1),u["\u0275\u0275property"]("@bodyExpansion",t._getExpandedState())("id",t.id),u["\u0275\u0275attribute"]("aria-labelledby",t._headerId),u["\u0275\u0275advance"](4),u["\u0275\u0275property"]("cdkPortalOutlet",t._portal))},directives:[w.c],styles:[".mat-expansion-panel{box-sizing:content-box;display:block;margin:0;border-radius:4px;overflow:hidden;transition:margin 225ms cubic-bezier(0.4, 0, 0.2, 1),box-shadow 280ms cubic-bezier(0.4, 0, 0.2, 1);position:relative}.mat-accordion .mat-expansion-panel:not(.mat-expanded),.mat-accordion .mat-expansion-panel:not(.mat-expansion-panel-spacing){border-radius:0}.mat-accordion .mat-expansion-panel:first-of-type{border-top-right-radius:4px;border-top-left-radius:4px}.mat-accordion .mat-expansion-panel:last-of-type{border-bottom-right-radius:4px;border-bottom-left-radius:4px}.cdk-high-contrast-active .mat-expansion-panel{outline:solid 1px}.mat-expansion-panel.ng-animate-disabled,.ng-animate-disabled .mat-expansion-panel,.mat-expansion-panel._mat-animation-noopable{transition:none}.mat-expansion-panel-content{display:flex;flex-direction:column;overflow:visible}.mat-expansion-panel-body{padding:0 24px 16px}.mat-expansion-panel-spacing{margin:16px 0}.mat-accordion>.mat-expansion-panel-spacing:first-child,.mat-accordion>*:first-child:not(.mat-expansion-panel) .mat-expansion-panel-spacing{margin-top:0}.mat-accordion>.mat-expansion-panel-spacing:last-child,.mat-accordion>*:last-child:not(.mat-expansion-panel) .mat-expansion-panel-spacing{margin-bottom:0}.mat-action-row{border-top-style:solid;border-top-width:1px;display:flex;flex-direction:row;justify-content:flex-end;padding:16px 8px 16px 24px}.mat-action-row button.mat-button-base,.mat-action-row button.mat-mdc-button-base{margin-left:8px}[dir=rtl] .mat-action-row button.mat-button-base,[dir=rtl] .mat-action-row button.mat-mdc-button-base{margin-left:0;margin-right:8px}\n"],encapsulation:2,data:{animation:[J.bodyExpansion]},changeDetection:0}),K),te=((U=function e(){r(this,e)}).\u0275fac=function(e){return new(e||U)},U.\u0275dir=u["\u0275\u0275defineDirective"]({type:U,selectors:[["mat-action-row"]],hostAttrs:[1,"mat-action-row"]}),U),ne=((q=function(){function e(t,n,i,o,a,s){var c=this;r(this,e),this.panel=t,this._element=n,this._focusMonitor=i,this._changeDetectorRef=o,this._animationMode=s,this._parentChangeSubscription=g.a.EMPTY;var d=t.accordion?t.accordion._stateChanges.pipe(Object(E.a)((function(e){return!(!e.hideToggle&&!e.togglePosition)}))):D.a;this._parentChangeSubscription=Object(T.a)(t.opened,t.closed,d,t._inputChanges.pipe(Object(E.a)((function(e){return!!(e.hideToggle||e.disabled||e.togglePosition)})))).subscribe((function(){return c._changeDetectorRef.markForCheck()})),t.closed.pipe(Object(E.a)((function(){return t._containsFocus()}))).subscribe((function(){return i.focusVia(n,"program")})),a&&(this.expandedHeight=a.expandedHeight,this.collapsedHeight=a.collapsedHeight)}return c(e,[{key:"_toggle",value:function(){this.disabled||this.panel.toggle()}},{key:"_isExpanded",value:function(){return this.panel.expanded}},{key:"_getExpandedState",value:function(){return this.panel._getExpandedState()}},{key:"_getPanelId",value:function(){return this.panel.id}},{key:"_getTogglePosition",value:function(){return this.panel.togglePosition}},{key:"_showToggle",value:function(){return!this.panel.hideToggle&&!this.panel.disabled}},{key:"_getHeaderHeight",value:function(){var e=this._isExpanded();return e&&this.expandedHeight?this.expandedHeight:!e&&this.collapsedHeight?this.collapsedHeight:null}},{key:"_keydown",value:function(e){switch(e.keyCode){case O.n:case O.f:Object(O.s)(e)||(e.preventDefault(),this._toggle());break;default:return void(this.panel.accordion&&this.panel.accordion._handleHeaderKeydown(e))}}},{key:"focus",value:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:"program",t=arguments.length>1?arguments[1]:void 0;this._focusMonitor.focusVia(this._element,e,t)}},{key:"ngAfterViewInit",value:function(){var e=this;this._focusMonitor.monitor(this._element).subscribe((function(t){t&&e.panel.accordion&&e.panel.accordion._handleHeaderFocus(e)}))}},{key:"ngOnDestroy",value:function(){this._parentChangeSubscription.unsubscribe(),this._focusMonitor.stopMonitoring(this._element)}},{key:"disabled",get:function(){return this.panel.disabled}}]),e}()).\u0275fac=function(e){return new(e||q)(u["\u0275\u0275directiveInject"](ee,1),u["\u0275\u0275directiveInject"](u.ElementRef),u["\u0275\u0275directiveInject"](j.h),u["\u0275\u0275directiveInject"](u.ChangeDetectorRef),u["\u0275\u0275directiveInject"]($,8),u["\u0275\u0275directiveInject"](P.a,8))},q.\u0275cmp=u["\u0275\u0275defineComponent"]({type:q,selectors:[["mat-expansion-panel-header"]],hostAttrs:["role","button",1,"mat-expansion-panel-header","mat-focus-indicator"],hostVars:15,hostBindings:function(e,t){1&e&&u["\u0275\u0275listener"]("click",(function(){return t._toggle()}))("keydown",(function(e){return t._keydown(e)})),2&e&&(u["\u0275\u0275attribute"]("id",t.panel._headerId)("tabindex",t.disabled?-1:0)("aria-controls",t._getPanelId())("aria-expanded",t._isExpanded())("aria-disabled",t.panel.disabled),u["\u0275\u0275styleProp"]("height",t._getHeaderHeight()),u["\u0275\u0275classProp"]("mat-expanded",t._isExpanded())("mat-expansion-toggle-indicator-after","after"===t._getTogglePosition())("mat-expansion-toggle-indicator-before","before"===t._getTogglePosition())("_mat-animation-noopable","NoopAnimations"===t._animationMode))},inputs:{expandedHeight:"expandedHeight",collapsedHeight:"collapsedHeight"},ngContentSelectors:B,decls:5,vars:1,consts:[[1,"mat-content"],["class","mat-expansion-indicator",4,"ngIf"],[1,"mat-expansion-indicator"]],template:function(e,t){1&e&&(u["\u0275\u0275projectionDef"](Y),u["\u0275\u0275elementStart"](0,"span",0),u["\u0275\u0275projection"](1),u["\u0275\u0275projection"](2,1),u["\u0275\u0275projection"](3,2),u["\u0275\u0275elementEnd"](),u["\u0275\u0275template"](4,N,1,1,"span",1)),2&e&&(u["\u0275\u0275advance"](4),u["\u0275\u0275property"]("ngIf",t._showToggle()))},directives:[k.t],styles:['.mat-expansion-panel-header{display:flex;flex-direction:row;align-items:center;padding:0 24px;border-radius:inherit;transition:height 225ms cubic-bezier(0.4, 0, 0.2, 1)}.mat-expansion-panel-header._mat-animation-noopable{transition:none}.mat-expansion-panel-header:focus,.mat-expansion-panel-header:hover{outline:none}.mat-expansion-panel-header.mat-expanded:focus,.mat-expansion-panel-header.mat-expanded:hover{background:inherit}.mat-expansion-panel-header:not([aria-disabled=true]){cursor:pointer}.mat-expansion-panel-header.mat-expansion-toggle-indicator-before{flex-direction:row-reverse}.mat-expansion-panel-header.mat-expansion-toggle-indicator-before .mat-expansion-indicator{margin:0 16px 0 0}[dir=rtl] .mat-expansion-panel-header.mat-expansion-toggle-indicator-before .mat-expansion-indicator{margin:0 0 0 16px}.mat-content{display:flex;flex:1;flex-direction:row;overflow:hidden}.mat-expansion-panel-header-title,.mat-expansion-panel-header-description{display:flex;flex-grow:1;margin-right:16px}[dir=rtl] .mat-expansion-panel-header-title,[dir=rtl] .mat-expansion-panel-header-description{margin-right:0;margin-left:16px}.mat-expansion-panel-header-description{flex-grow:2}.mat-expansion-indicator::after{border-style:solid;border-width:0 2px 2px 0;content:"";display:inline-block;padding:3px;transform:rotate(45deg);vertical-align:middle}\n'],encapsulation:2,data:{animation:[J.indicatorRotate]},changeDetection:0}),q),ie=((V=function e(){r(this,e)}).\u0275fac=function(e){return new(e||V)},V.\u0275dir=u["\u0275\u0275defineDirective"]({type:V,selectors:[["mat-panel-description"]],hostAttrs:[1,"mat-expansion-panel-header-description"]}),V),oe=((L=function e(){r(this,e)}).\u0275fac=function(e){return new(e||L)},L.\u0275dir=u["\u0275\u0275defineDirective"]({type:L,selectors:[["mat-panel-title"]],hostAttrs:[1,"mat-expansion-panel-header-title"]}),L),ae=((z=function(e){t(o,e);var n=i(o);function o(){var e;return r(this,o),(e=n.apply(this,arguments))._ownHeaders=new u.QueryList,e._hideToggle=!1,e.displayMode="default",e.togglePosition="after",e}return c(o,[{key:"ngAfterContentInit",value:function(){var e=this;this._headers.changes.pipe(Object(A.a)(this._headers)).subscribe((function(t){e._ownHeaders.reset(t.filter((function(t){return t.panel.accordion===e}))),e._ownHeaders.notifyOnChanges()})),this._keyManager=new j.g(this._ownHeaders).withWrap()}},{key:"_handleHeaderKeydown",value:function(e){var t=e.keyCode,n=this._keyManager;t===O.h?Object(O.s)(e)||(n.setFirstItemActive(),e.preventDefault()):t===O.e?Object(O.s)(e)||(n.setLastItemActive(),e.preventDefault()):this._keyManager.onKeydown(e)}},{key:"_handleHeaderFocus",value:function(e){this._keyManager.updateActiveItem(e)}},{key:"hideToggle",get:function(){return this._hideToggle},set:function(e){this._hideToggle=Object(h.c)(e)}}]),o}(b)).\u0275fac=function(e){return re(e||z)},z.\u0275dir=u["\u0275\u0275defineDirective"]({type:z,selectors:[["mat-accordion"]],contentQueries:function(e,t,n){var i;1&e&&u["\u0275\u0275contentQuery"](n,ne,!0),2&e&&u["\u0275\u0275queryRefresh"](i=u["\u0275\u0275loadQuery"]())&&(t._headers=i)},hostAttrs:[1,"mat-accordion"],hostVars:2,hostBindings:function(e,t){2&e&&u["\u0275\u0275classProp"]("mat-accordion-multi",t.multi)},inputs:{multi:"multi",displayMode:"displayMode",togglePosition:"togglePosition",hideToggle:"hideToggle"},exportAs:["matAccordion"],features:[u["\u0275\u0275ProvidersFeature"]([{provide:G,useExisting:z}]),u["\u0275\u0275InheritDefinitionFeature"]]}),z),re=u["\u0275\u0275getInheritedFactory"](ae),se=((X=function e(){r(this,e)}).\u0275mod=u["\u0275\u0275defineNgModule"]({type:X}),X.\u0275inj=u["\u0275\u0275defineInjector"]({factory:function(e){return new(e||X)},imports:[[k.c,_,w.i]]}),X)}}])}();