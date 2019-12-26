import React from 'react'

export const MyFormControl = ({ formcontrol, errorslist }) => {

    if (!errorslist) {
        return formcontrol
    }

    return (
        <div>
            {formcontrol}
            {errorslist.map((error, index) => <li id="errorBlock" key={index}>{error}</li>)}
        </div>
    )
}