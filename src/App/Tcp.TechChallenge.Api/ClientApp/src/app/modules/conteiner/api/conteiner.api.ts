import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { ApiOperations } from "../../../shared/constants/api-operations";
import { StringExtensions } from "../../../shared/extensions/string.extensions";
import { Conteiner } from "../models/conteiner";

@Injectable()
export class ConteinerApi {
    constructor(public http: HttpClient) {
    }

    public create(conteiner: Conteiner) {
        return this.http.post(ApiOperations.Conteiner.Create, conteiner);
    }

    public edit(identifier:string, conteiner: Conteiner) {
        return this.http.put(
            StringExtensions.format(ApiOperations.Conteiner.Edit, [identifier]), conteiner);
    }

    public delete(identifier:string) {
        return this.http.delete(
            StringExtensions.format(ApiOperations.Conteiner.Delete, [identifier]));
    }

    public findConteinerByIdentifier(identifier:string) {
        return this.http.get<Conteiner>(StringExtensions.format(ApiOperations.Conteiner.FindById, [identifier]));
    }

    public findAllConteiners() {
        return this.http.get<Conteiner[]>(ApiOperations.Conteiner.All);
    }
}