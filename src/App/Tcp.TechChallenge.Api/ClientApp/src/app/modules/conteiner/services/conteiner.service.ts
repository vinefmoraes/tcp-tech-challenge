import { Injectable } from "@angular/core";
import { ConteinerApi } from "../api/conteiner.api";
import { Conteiner } from "../models/conteiner";

@Injectable()
export class ConteinerService{
    constructor(public api: ConteinerApi){
    }

    public create(conteiner: Conteiner) {
        return this.api.create(conteiner);
    }

    public edit(identifier:string, conteiner: Conteiner) {
        return this.api.edit(identifier, conteiner);
    }

    public delete(identifier:string) {
        return this.api.delete(identifier);
    }

    public findAllConteiners() {
        return this.api.findAllConteiners();
    }

    public findConteinerByIdentifier(identifier:string) {
        return this.api.findConteinerByIdentifier(identifier);
    }
}