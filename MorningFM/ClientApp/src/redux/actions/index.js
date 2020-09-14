import { SET_USERAUTHENTICATING} from "../action-constants/action-types"

export function setIsUserAuthenticating(payload){
    return { type: SET_USERAUTHENTICATING, payload}
};