import { useEffect } from "react"
import { useNavigate } from "react-router-dom"


export const DefaultPage = () =>{

    let navigate = useNavigate();

    useEffect(() =>{
        navigate('/login');
    });

    return (
        <div>

        </div>
    )
}