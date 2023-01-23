import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../../features/auth/authSlice";

export const DefaultPage = () =>{
    let navigate = useNavigate();
    const token = useSelector(selectCurrentToken);

    useEffect(() =>{
        let url = token ? '/main' : '/login';
        navigate(url);
    });

    return (
        <div>

        </div>
    )
}