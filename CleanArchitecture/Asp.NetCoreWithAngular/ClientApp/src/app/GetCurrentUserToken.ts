import { JwtHelperService } from '@auth0/angular-jwt';

export class GetCurrentUserToken {

  constructor() { }

  private helper = new JwtHelperService();

  public GetUserToken(): any {
    var LocalStorageToken = localStorage.getItem('token');
    const LocalToken = this.helper.urlBase64Decode(LocalStorageToken!.split('.')[1]);
    var TokenData = JSON.parse(LocalToken);
    //alert(JSON.stringify(TokenData.UserID + "***" + TokenData.unique_name + "***" + TokenData.family_name));
    //alert(JSON.stringify("UserID:" + TokenData.UserID + "RoleID:" + TokenData.RoleID + "***" + TokenData.unique_name + "***" + TokenData.family_name));
    return TokenData;
  }
}
