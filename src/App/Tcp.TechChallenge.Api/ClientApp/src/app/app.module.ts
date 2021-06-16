import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ConteinerService } from './modules/conteiner/services/conteiner.service';
import { ConteinerApi } from './modules/conteiner/api/conteiner.api';
import { CreateConteinerComponent } from './create/create.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    CreateConteinerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'edit', component: FetchDataComponent },
      { path: 'new', component: CreateConteinerComponent }
    ])
  ],
  providers: [
    ConteinerService,
    ConteinerApi
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
