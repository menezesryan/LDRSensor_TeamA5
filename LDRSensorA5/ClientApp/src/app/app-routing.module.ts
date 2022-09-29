import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutomaticModeComponent } from './automatic-mode/automatic-mode.component';
import { HomeComponent } from './home/home.component';
import { ManualModeComponent } from './manual-mode/manual-mode.component';


const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'automatic-mode', component: AutomaticModeComponent },
  { path: 'manual-mode', component: ManualModeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
