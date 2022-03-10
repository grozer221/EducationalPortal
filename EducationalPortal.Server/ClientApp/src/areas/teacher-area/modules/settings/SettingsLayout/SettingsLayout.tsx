import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {Error} from '../../../../../components/Error/Error';
import {SettingsApp} from '../pages/SettingsApp/SettingsApp';
import {WithAdministratorRoleOrRender} from '../../../../../hocs/WithAdministratorRoleOrRender';
import {SettingsMy} from '../pages/SettingsMy/SettingsMy';

export const SettingsLayout: FC = () => {
    return (
        // <Routes>
        //     <Route path={'/my'} element={<SettingsMy/>}/>
        //     <Route path={'/site'} element={
        //         <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
        //             <SettingsApp/>
        //         </WithAdministratorRoleOrRender>
        //     }/>
        //     <Route path={'*'} element={<Error/>}/>
        // </Routes>
        <Routes>
            <Route path={'/my'} element={<SettingsMy/>}/>
            <Route path={'/site'} element={
                    <SettingsApp/>
            }/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
