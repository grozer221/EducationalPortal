import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {Error} from '../../../../../components/Error/Error';
import {GradesIndex} from '../pages/GradesIndex/GradesIndex';
import {GradesCreate} from '../pages/GradesCreate/GradesCreate';
import {GradesView} from '../pages/GradesView/GradesView';
import {GradesUpdate} from '../pages/GradesUpdate/GradesUpdate';

export const GradesLayout: FC = () => {
    return (
        <Routes>
            <Route path={'/'} element={<GradesIndex/>}/>
            <Route path={':id'} element={<GradesView/>}/>
            <Route path={'create'} element={<GradesCreate/>}/>
            <Route path={'update/:id'} element={<GradesUpdate/>}/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
