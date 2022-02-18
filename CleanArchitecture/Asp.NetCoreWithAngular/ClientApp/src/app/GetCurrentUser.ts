import { JwtHelperService } from '@auth0/angular-jwt';

export class GetCurrentUser {

  constructor() { }

  private helper = new JwtHelperService();

  public GetUserID(): string {
    var LocalStorageToken = localStorage.getItem('token');
    const LocalToken = this.helper.urlBase64Decode(LocalStorageToken!.split('.')[1]);
    var TokenData = JSON.parse(LocalToken);
    //alert(JSON.stringify(TokenData.UserID + "***" + TokenData.unique_name + "***" + TokenData.family_name));
    return TokenData.UserID;
  }
}
