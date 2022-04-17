import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { AddInterfaceVM } from "./viewmodels";
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
            name: new FormControl(this.value.name, [
                Validators.required,
                Validators.minLength(1)
            ]),
            description: new FormControl(this.value.description, [
                Validators.required,
                Validators.minLength(1)
            ]),
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

    public fillUp() {
        this.value.name = this.form.value.name;
        this.value.description = this.form.value.description;
    }
}
