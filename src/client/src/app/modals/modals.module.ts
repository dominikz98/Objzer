import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { IonicModule } from "@ionic/angular";
import { EditPropertyModalPage } from "./edit-property-modal/edit-property-modal.page";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        IonicModule,
        ReactiveFormsModule,
    ],
    declarations: [
        EditPropertyModalPage,
    ],
    exports: [
        EditPropertyModalPage
    ]
})
export class ModalsModule { }