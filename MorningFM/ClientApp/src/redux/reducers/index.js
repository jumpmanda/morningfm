import { SET_USERAUTHENTICATING } from "../action-constants/action-types";

const initialState = {
    isUserAuthenticating: false
}

function rootReducer(state = initialState, action){
    if (action.type === SET_USERAUTHENTICATING){
        return Object.assign({}, state, {
            isUserAuthenticating: action.payload
        });
    }
    return state; 
}

export default rootReducer;