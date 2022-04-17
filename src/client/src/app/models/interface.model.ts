import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { AddInterfaceVM, AddPropertyVM } from "../endpoints/viewmodels";
import { PropertyModel } from "./property.model";

export class InterfaceModel {
    public form: FormGroup;
    public value: AddInterfaceVM;

    constructor(value?: AddInterfaceVM) {
        this.value = value;
        if (this.value == null) {
            this.value = new AddInterfaceVM();
        }

        this.form = new FormGroup({
            name: new FormControl(this.value.name),
            description: new FormControl(this.value.description),
            properties: new FormArray([])
        });
    }

    public attachProperty(value: PropertyModel) {
        const properties = this.form.controls["properties"] as FormArray;
        properties.push(value.form);

        if (this.value.properties == null) {
            this.value.properties = [];
        }
        this.value.properties.push(value.value)
    }
}
