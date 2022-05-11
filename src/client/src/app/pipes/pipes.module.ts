import { NgModule } from "@angular/core";
import { TruncatePipe } from "./TruncatePipe";

@NgModule({
  declarations: [
    TruncatePipe,
  ],
  exports: [
    TruncatePipe
  ]
})
export class PipesModule { }