import { Menu } from "./Menu";

export interface UserMenu {
    userId: number;
    menuId: number;
    registeredUserId: number;
    menu: Menu;
}